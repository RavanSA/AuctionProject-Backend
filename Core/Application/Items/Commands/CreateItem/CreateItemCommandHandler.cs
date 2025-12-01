namespace Application.Items.Commands.CreateItem
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Interfaces;
    using Common.Models;
    using Domain.Entities;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore; // for AnyAsync in validation

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Response<ItemResponseModel>>
    {
        private readonly IAuctionSystemDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CreateItemCommandHandler> logger;

        public CreateItemCommandHandler(
            IAuctionSystemDbContext context,
            IMapper mapper,
            ILogger<CreateItemCommandHandler> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<Response<ItemResponseModel>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
{
    logger.LogInformation("Starting CreateItemCommand for UserId={UserId}, CategoryId={CategoryId}, SubCategoryId={SubCategoryId}",
        request.UserId, request.CategoryId, request.SubCategoryId);

    // Validate foreign keys before saving
    var userExists = await context.Users.AnyAsync(x => x.Id == request.UserId, cancellationToken);
    var categoryExists = await context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken);
    var subCategoryExists = await context.SubCategories.AnyAsync(x => x.Id == request.SubCategoryId, cancellationToken);

    logger.LogInformation("FK validation results → UserExists={UserExists}, CategoryExists={CategoryExists}, SubCategoryExists={SubCategoryExists}",
        userExists, categoryExists, subCategoryExists);

    if (!userExists || !categoryExists || !subCategoryExists)
    {
        logger.LogWarning("Foreign key check failed. Cannot insert item.");
        return new Response<ItemResponseModel>("Invalid foreign key reference detected.");
    }

    try
    {
        var item = mapper.Map<Item>(request);
        item.StartTime = item.StartTime.ToUniversalTime();
        item.EndTime = item.EndTime.ToUniversalTime();

        logger.LogInformation("Mapped Item ready for insert: {@Item}", item);

        await context.Items.AddAsync(item, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Item inserted successfully (RowsAffected={RowsAffected}, ItemId={ItemId})", result, item.Id);
        return new Response<ItemResponseModel>(new ItemResponseModel(item.Id));
    }
    catch (DbUpdateException dbEx)
    {
        logger.LogError(dbEx, "DbUpdateException while inserting Item. Likely FK or constraint violation.");
        return new Response<ItemResponseModel>("Database update failed: " + (dbEx.InnerException?.Message ?? dbEx.Message));
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Unexpected error while creating Item");
        return new Response<ItemResponseModel>("Unexpected error: " + ex.Message);
    }
}
    }
}