using SQLite;
using System;

namespace mobilki
{
    [Table("Category")]
    public class Category
    {
        public String Name { get; set; }
        public int IdIcon { get; set; }
        [PrimaryKey, AutoIncrement, Column("_idCategory")]
        public int IdCategory { get; set; }
        public String IdUser { get; set; }

        public Category() { }
        public Category(int idCategory, String name, int idIcon, String idUser)
        {

            this.Name = name;
            this.IdIcon = idIcon;
            this.IdCategory = idCategory;
            this.IdUser = idUser;

        }

        public Category(String name, int idIcon, String idUser)
        {

            this.Name = name;
            this.IdIcon = idIcon;
            this.IdUser = idUser;

        }
    }
}
