import {
  ITEMS_ENDPOINT,
  PAGE_HEADER_NAME,
  PAGESIZE_HEADER_NAME
} from "@/api/apiConfig";
import apiService from "@/api/apiService";

const itemQueries = {
  async pagedList(page: string, pageSize: string) {
    apiService.setHeader(PAGE_HEADER_NAME, page);
    apiService.setHeader(PAGESIZE_HEADER_NAME, pageSize);
    return await apiService.query(ITEMS_ENDPOINT);
  },

  async all() {
    return await apiService.query(`${ITEMS_ENDPOINT}/all`);
  },

  async get(id: string) {
    return await apiService.get(ITEMS_ENDPOINT, id);
  }
};

export default itemQueries;
