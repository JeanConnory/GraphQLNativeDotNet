using Movies.Models;

namespace Movies.GraphQL.Client.DTO;

public class MoviesResponse
{
    public List<Movie> Movies { get; set; }
}
