import Axios, { AxiosResponse } from "axios";
import Tournament from "@/models/tournament.interface";
import Patch from '@/models/meta/patch.interface';
import { BaseService } from './base.service';

class TournamentService extends BaseService {
  private static instance: TournamentService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<Tournament[]>> {
    return Axios.get(`${this.api}/tournaments`);
  }

  public post(tournament: Tournament): Promise<AxiosResponse<Tournament>> {
    return Axios.post(`${this.api}/tournaments`, tournament);
  }

  public put(id: number, tournament: Tournament): Promise<AxiosResponse<Tournament>> {
    return Axios.put(`${this.api}/tournaments/${id}`, tournament);
  }

  public patch(id: number, patch: Patch[]): Promise<AxiosResponse<Tournament>> {
    return Axios.patch(`${this.api}/tournaments/${id}`, patch);
  }

  public delete(id: number): Promise<AxiosResponse> {
    return Axios.delete(`${this.api}/tournaments/${id}`);
  }
}

// export a singleton instance in the global namespace
export const TournamentServiceInstance = TournamentService.Instance;
