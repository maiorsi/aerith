import Axios, { AxiosResponse } from "axios";
import Team from "@/models/team.interface";
import Patch from '@/models/meta/patch.interface';
import { BaseService } from './base.service';

class TeamService extends BaseService {
  private static instance: TeamService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<Team[]>> {
    return Axios.get(`${this.api}/teams`);
  }

  public post(team: Team): Promise<AxiosResponse<Team>> {
    return Axios.post(`${this.api}/teams`, team);
  }

  public put(id: number, team: Team): Promise<AxiosResponse<Team>> {
    return Axios.put(`${this.api}/teams/${id}`, team);
  }

  public patch(id: number, patch: Patch[]): Promise<AxiosResponse<Team>> {
    return Axios.patch(`${this.api}/teams/${id}`, patch);
  }

  public delete(id: number): Promise<AxiosResponse> {
    return Axios.delete(`${this.api}/teams/${id}`);
  }
}

// export a singleton instance in the global namespace
export const TeamServiceInstance = TeamService.Instance;
