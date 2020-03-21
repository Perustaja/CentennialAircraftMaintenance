using System.Collections.Generic;
using AutoMapper;
using CAM.Core.Interfaces;
using CAM.Core.SharedKernel;

namespace CAM.Web.Mapping
{
    public class PaginatedListMapper : IPaginatedListMapper
    {
        private readonly IMapper _mapper;
        public PaginatedListMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// MapToViewModelList&lt;T outType, U inType&gt;(original) returns a PaginatedList with the internals
        /// mapped to a new type. The two objects must be configured to map via AutoMapper.
        /// </summary>
        public PaginatedList<T> MapToViewModelList<T, U>(PaginatedList<U> orig)
        {
            List<T> convertedList = new List<T>();
            foreach (U obj in orig)
            {
                convertedList.Add(_mapper.Map<T>(obj));
            }
            return new PaginatedList<T>(convertedList, convertedList.Count, 
            orig.PageIndex, orig.ItemsPerPage);
        }
    }
}