using System;


namespace R5T.L0028
{
    public class PublicationNameOperator : IPublicationNameOperator
    {
        #region Infrastructure

        public static IPublicationNameOperator Instance { get; } = new PublicationNameOperator();


        private PublicationNameOperator()
        {
        }

        #endregion
    }
}
