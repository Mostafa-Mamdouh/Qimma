namespace PRJ.GlobalComponents.Const
{
    public abstract class ConstErrors
    {
        //00
        public const string UnhandeledExceptionCode = "0000";
        public const string UnhandeledExceptionMessage = "Unknown Error: {0}";
        //01
        public const string ParameterExceptionTypeMissingCode = "0101";
        public const string ParameterExceptionTypeWrongFormatCode = "0102";
        public const string ParameterExceptionTypeMissingMessage = "Parameter [{0}] is missing";
        public const string ParameterExceptionTypeWrongFormatMessage = "Parameter [{0}] format is wrong";
        //02
        public const string NotFound = "No Data Found";
        public const string NodeHasChildren = "Node Has Children";
        public const string TransactionSubmitted = "Transaction Already Submitted";


        public const string EntityIdNotExist = "Entity Id is not exist, Please insert Valid Entity Id";
        public const string LicenseIdNotExist = "Liecense Id is not exist, Please insert Valid Liecense Id";

        public const string FacilityIdNotExist = "Facility Id is not exist, Please insert Valid Facility Id";


    }
}
