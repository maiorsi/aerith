import Axios, { AxiosResponse } from "axios";
import Profile from "@/models/profile.interface";
import { BaseService } from './base.service';

class ProfileService extends BaseService {
  private static instance: ProfileService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<Profile>> {
    return Axios.get(`${this.api}/account/profile`);
  }
}

// export a singleton instance in the global namespace
export const ProfileServiceInstance = ProfileService.Instance;
