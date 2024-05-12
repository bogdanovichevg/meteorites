import axios from "axios";
import { MeteoritesFiltersReq } from "../../models";

export class MeteoriteApi {
  #baseUrl: string;

  constructor() {
    this.#baseUrl = "http://localhost:4000/Meteorites/";
  }

  async getMeteorites(req: MeteoritesFiltersReq) {
    try {
      const params = new URLSearchParams();
      params.append("fromYear", req.fromYear.toString());
      params.append("toYear", req.toYear.toString());
      params.append("meteoriteName", req.meteoriteName);
      params.append("meteoriteClass", req.meteoriteClass);
      params.append("sortableField", req.sortableField);
      params.append("isDesc", req.isDesc.toString());
      params.append("take", req.take.toString());
      params.append("skip", req.skip.toString());
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
