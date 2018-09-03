using System.Data;
using ClientCore.DB;

namespace ClientCore.DB
{
    public class UserTableData: BaseTableData
    {
        public int ID;
        public string Nick;
        public string Password;
        public int Authority;

        public UserTableData():this(0)
        {
        }

        public UserTableData(int ID)
        {
            this.ID = ID;
        }

        public override void ReadFrom(DataRow collection)
        {
            base.ReadFrom(collection);
            ID = (int)collection[0];
            Nick = collection[1] as string;
            Password = collection[2] as string;
            Authority = (int)collection[3];
        }
    }
}