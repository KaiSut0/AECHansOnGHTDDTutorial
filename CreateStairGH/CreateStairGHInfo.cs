using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace CreateStairGH
{
    public class CreateStairGHInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "CreateStairGH";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("0fea02ed-f35a-4ec4-9113-d546184dad1d");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
