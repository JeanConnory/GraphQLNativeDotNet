using GraphQL;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.GraphQL.Types.Enums;

namespace Movies.API.GraphQL.Types;

public class MovieType : ObjectGraphType<Movie>
{
    public MovieType(MovieDbContext db, IDataLoaderContextAccessor dataLoader)
    {
        Field(a => a.Id).Description("Id of Movie");
        Field(a => a.Name).Description("Name of Movie");
        Field(a => a.Description).Description("Description of Movie");
        Field(a => a.LaunchDate).Description("LaunchDate of Movie");
        Field<MovieGenreType>("genre").Description("Genre of Movie");
        Field<ListGraphType<MovieReviewType>>("reviews").Description("Reviews of Movie")
            .Resolve(context =>
            {
                if (context.Source.Reviews != null)
                    return context.Source.Reviews;

                var loader = dataLoader.Context
                    .GetOrAddCollectionBatchLoader<int, MovieReview>("GetMovieReviewsByMovieId",
                        async movieIds =>
                        {
                            var reviews = await db.Reviews.Where(a => movieIds.Contains(a.MovieId)).ToListAsync();
                            return reviews.ToLookup(a => a.MovieId);
                        });

                var movieId = context.Source.Id;
                return loader.LoadAsync(movieId);
            });
    }
}
