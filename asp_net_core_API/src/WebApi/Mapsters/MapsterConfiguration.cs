using Mapster;
using store_vegetable.Core.DTO;
using store_vegetable.Core.Entites;
using WebApi.Models;

namespace WebApi.Mapsters
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CategoryDto, Categories>();
            config.NewConfig<CategoryItem, Categories>();
            config.NewConfig<CategoryEditModel, Categories>();


            config.NewConfig<FoodDto, Food>();
            config.NewConfig<FoodQuery,FoodFilterModel>();
            config.NewConfig<CategoryQuery, CategoryFilter>();

            config.NewConfig<FoodEditModel, Food>();

            config.NewConfig<Feedback, FeedbackQuery>();
            config.NewConfig<FeedbackDto, Feedback>();
            config.NewConfig<FeedbackQuery, Feedback>();
            config.NewConfig<FeedbackQuery, FeedbackFilterModel>();

            config.NewConfig<OrderEditModel, Order>();

            config.NewConfig<OrderQuery, OrderFilterDto>();

            config.NewConfig<OrderItem, OrderItemDto>();

            config.NewConfig<UserQuery, UserFilterModel>();
            config.NewConfig<User, UserDto>();

        }
    }
}
