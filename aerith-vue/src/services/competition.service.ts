import Axios, { AxiosResponse } from "axios";
import Competition from "@/models/competition.interface";
import Patch from '@/models/meta/patch.interface';
import { BaseService } from './base.service';

class CompetitionService extends BaseService {
  private static instance: CompetitionService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<Competition[]>> {
    return Axios.get(`${this.api}/competitions`);
  }

  public post(competition: Competition): Promise<AxiosResponse<Competition>> {
    return Axios.post(`${this.api}/competitions`, competition);
  }

  public put(id: number, competition: Competition): Promise<AxiosResponse<Competition>> {
    return Axios.put(`${this.api}/competitions/${id}`, competition);
  }

  public patch(id: number, patch: Patch[]): Promise<AxiosResponse<Competition>> {
    return Axios.patch(`${this.api}/competitions/${id}`, patch);
  }

  public delete(id: number): Promise<AxiosResponse> {
    return Axios.delete(`${this.api}/competitions/${id}`);
  }
}

// export a singleton instance in the global namespace
export const CompetitionServiceInstance = CompetitionService.Instance;
