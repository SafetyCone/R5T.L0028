using System;

using R5T.T0132;


namespace R5T.L0028
{
    [FunctionalityMarker]
    public partial interface IPublicationNameOperator : IFunctionalityMarker
    {
        public string GetPublicationName(string projectName)
        {
            // Just use the project name, assuming the project name is unique.
            var publicationName = projectName;
            return publicationName;
        }
    }
}
