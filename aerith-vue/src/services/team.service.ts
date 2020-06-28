import Axios, { AxiosResponse } from "axios";
import Team from "@/models/team.interface";

class TeamService {
  private static instance: TeamService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<Team[]>> {
    return Axios.get("https://localhost:5001/api/v1/teams");
  }

  public patch(id: number, patch: any): Promise<AxiosResponse<Team>> {
    return Axios.patch(`https://localhost:5001/api/v1/teams/${id}`, patch);
  }
}

// export a singleton instance in the global namespace
export const TeamServiceInstance = TeamService.Instance;
