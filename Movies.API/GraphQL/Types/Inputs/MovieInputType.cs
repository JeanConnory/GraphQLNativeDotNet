using Movies.API.GraphQL.Types.Enums;

namespace Movies.API.GraphQL.Types.Inputs
{
    public class MovieInputType : InputObjectGraphType<Movie>
    {
        public MovieInputType()
        {
            Field(a => a.Name).Description("Name of Movie");
            Field(a => a.Description).Description("Description of Movie");
            Field(a => a.LaunchDate).Description("LaunchDate of Movie");
            Field<MovieGenreType>("genre").Description("Genre of Movie");
        }
    }
}
