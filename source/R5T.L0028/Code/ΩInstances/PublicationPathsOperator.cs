using System;


namespace R5T.L0028
{
    public class PublicationPathsOperator : IPublicationPathsOperator
    {
        #region Infrastructure

        public static IPublicationPathsOperator Instance { get; } = new PublicationPathsOperator();


        private PublicationPathsOperator()
        {
        }

        #endregion
    }
}
