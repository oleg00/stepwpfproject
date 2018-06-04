using DatabaseLib;
namespace AdminWindow
{
    internal class CategoriesOfFilmToAdd_cb 
    // This class is made for 'Edit Films Window' to choose multiple categories for adding films.
    {
        public Categories Category { get; set; }
        public bool Is_Chosen { get; set; }

        public string Title => Category.title;

        public CategoriesOfFilmToAdd_cb(Categories c)
        {
            Category = c;
            Is_Chosen = false;
        }
    }
}