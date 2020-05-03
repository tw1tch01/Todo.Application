import { ITEMS_ENDPOINT } from "@/api/apiConfig";
import apiService from "@/api/apiService";

const itemCommands = {
  async addItem(item) {
    return await apiService.post(ITEMS_ENDPOINT, item);
  },

  async updateItem(itemId: string, item) {
    return await apiService.patch(`${ITEMS_ENDPOINT}/${itemId}`, item);
  },

  async deleteItem(itemId: string) {
    return await apiService.delete(`${ITEMS_ENDPOINT}/${itemId}`);
  },

  async addChildItem(itemId: string, item) {
    return await apiService.post(`${ITEMS_ENDPOINT}/${itemId}`, item);
  },

  async cancelItem(itemId: string) {
    return await apiService.post(`${ITEMS_ENDPOINT}/${itemId}/cancel`);
  },

  async completeItem(itemId: string) {
    return await apiService.post(`${ITEMS_ENDPOINT}/${itemId}/complete`);
  },

  async resetItem(itemId: string) {
    return await apiService.post(`${ITEMS_ENDPOINT}/${itemId}/reset`);
  },

  async startItem(itemId: string) {
    return await apiService.post(`${ITEMS_ENDPOINT}/${itemId}/start`);
  }
};

export default itemCommands;
