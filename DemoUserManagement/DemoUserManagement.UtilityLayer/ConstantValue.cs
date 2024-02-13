using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.UtilityLayer
{

    public class ConstantValues
    {

    }

    public struct AddressType
    {
        public const int CurrentAddress = 1;
        public const int PermanentAddress = 2;
    }

    public struct EducationType
    {
        public const int MatriculationEducation = 1;
        public const int IntermediateEducation = 2;
        public const int GraduateEducation = 3;
    }

    //editing student=1
    public struct NoteType
    {
        public const int ObjectType = 1;
    }

}
