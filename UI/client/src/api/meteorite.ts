import axios from "axios";
import { ReqGetMeteorites } from "../models";

export class MeteoriteApi {
  #baseUrl: string;

  constructor() {
    this.#baseUrl = "http://localhost:4000/Meteorites/";
  }

  async getMeteorites(req: ReqGetMeteorites) {
    try {
      const params = new URLSearchParams();
      params.append("FromYear", req.fromYear.toString());
      params.append("ToYear", req.toYear.toString());
      params.append("MeteoriteName", req.meteoriteName);
      params.append("ClassName", req.className);
      params.append("SortField", req.sortField);
      params.append("Take", req.take.toString());
      params.append("Skip", req.skip.toString());
      params.append("IsDesc", req.isDesc.toString());
      const response = await axios.get(`${this.#baseUrl}GetMeteorites`, {
        params,
      });
      return response.data;
    } catch (err) {
      return [];
    }
  }

  async getMeteoritesClasses() {
    const response = await axios.get(`${this.#baseUrl}GetMeteoritesClasses`);
    return response.data;
  }
}
