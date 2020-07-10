import Axios, { AxiosResponse } from "axios";
import League from "@/models/league.interface";
import Patch from '@/models/meta/patch.interface';
import { BaseService } from './base.service';

class LeagueService extends BaseService {
  private static instance: LeagueService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<League[]>> {
    return Axios.get(`${this.api}/leagues/all`);
  }

  public post(league: League): Promise<AxiosResponse<League>> {
    return Axios.post(`${this.api}/leagues`, league);
  }

  public put(id: number, league: League): Promise<AxiosResponse<League>> {
    return Axios.put(`${this.api}/leagues/${id}`, league);
  }

  public patch(id: number, patch: Patch[]): Promise<AxiosResponse<League>> {
    return Axios.patch(`${this.api}/leagues/${id}`, patch);
  }

  public delete(id: number): Promise<AxiosResponse> {
    return Axios.delete(`${this.api}/leagues/${id}`);
  }
}

// export a singleton instance in the global namespace
export const LeagueServiceInstance = LeagueService.Instance;
