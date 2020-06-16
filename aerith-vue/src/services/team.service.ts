import Axios, { AxiosResponse } from "axios";

class TeamService {
  private static instance: TeamService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<any>> {
    return Axios.get("https://localhost:5001/api/v1/teams");
  }
}

// export a singleton instance in the global namespace
export const TeamServiceInstance = TeamService.Instance;
