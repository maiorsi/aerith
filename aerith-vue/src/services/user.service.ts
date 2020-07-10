import Axios, { AxiosResponse } from "axios";
import User from "@/models/user.interface";
import Patch from '@/models/meta/patch.interface';
import { BaseService } from './base.service';

class UserService extends BaseService {
  private static instance: UserService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<User[]>> {
    return Axios.get(`${this.api}/users/all`);
  }

  public post(user: User): Promise<AxiosResponse<User>> {
    return Axios.post(`${this.api}/users`, user);
  }

  public put(id: number, user: User): Promise<AxiosResponse<User>> {
    return Axios.put(`${this.api}/users/${id}`, user);
  }

  public patch(id: number, patch: Patch[]): Promise<AxiosResponse<User>> {
    return Axios.patch(`${this.api}/users/${id}`, patch);
  }

  public delete(id: number): Promise<AxiosResponse> {
    return Axios.delete(`${this.api}/users/${id}`);
  }
}

// export a singleton instance in the global namespace
export const UserServiceInstance = UserService.Instance;
