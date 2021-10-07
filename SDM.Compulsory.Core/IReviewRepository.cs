using System.Collections.Generic;

namespace SDM.Compulsory.Core
{
    public interface IReviewRepository
    {
        Review Add(Review review);
        List<Review> GetAll();
        Review GetbyId(int id);
        Review Update(Review review);
        Review Delete(Review review);
    }
}