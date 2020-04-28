// class ItemLookupParams
// {
//     constructor(
//         createdAfter: string = "",
//         createdBefore: string = "",
//         searchBy: string = "",
//         itemIds: Array<string> = [],
//         filterByStatus: string = "",
//         filterByImportance: string = "",
//         filterByPriority: string = "",
//         sortBy: string = ""
//     )
//     {
//         this.createdAfter = createdAfter;
//         this.createdBefore = createdBefore;
//         this.searchBy = searchBy;
//         this.itemIds = itemIds;
//         this.filterByStatus = filterByStatus;
//         this.filterByImportance = filterByImportance;
//         this.filterByPriority = filterByPriority;
//         this.sortBy = sortBy;
//     }

//     createdAfter: string;
//     createdBefore: string;
//     searchBy: string;
//     itemIds: Array<string>;
//     filterByStatus: string;
//     filterByImportance: string;
//     filterByPriority: string;
//     sortBy: string;

//     toQueryObject()
//     {
//         return {
//             createdAfter: this.createdAfter,
//             createdBefore: this.createdBefore,
//             searchBy: this.searchBy,
//             itemIds: this.itemIds,
//             filterByStatus: this.filterByStatus,
//             filterByImportance: this.filterByImportance,
//             filterByPriority: this.filterByPriority,
//             sortBy: this.sortBy
//         }
//     }
// };

// export default ItemLookupParams;