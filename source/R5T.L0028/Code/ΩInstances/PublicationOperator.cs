using System;


namespace R5T.L0028
{
    public class PublicationOperator : IPublicationOperator
    {
        #region Infrastructure

        public static IPublicationOperator Instance { get; } = new PublicationOperator();


        private PublicationOperator()
        {
        }

        #endregion
    }
}
