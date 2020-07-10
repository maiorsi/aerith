import Axios, { AxiosResponse } from "axios";
import Group from "@/models/group.interface";
import Patch from '@/models/meta/patch.interface';
import { BaseService } from './base.service';

class GroupService extends BaseService {
  private static instance: GroupService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<Group[]>> {
    return Axios.get(`${this.api}/groups`);
  }

  public post(group: Group): Promise<AxiosResponse<Group>> {
    return Axios.post(`${this.api}/groups`, group);
  }

  public put(id: number, group: Group): Promise<AxiosResponse<Group>> {
    return Axios.put(`${this.api}/groups/${id}`, group);
  }

  public patch(id: number, patch: Patch[]): Promise<AxiosResponse<Group>> {
    return Axios.patch(`${this.api}/groups/${id}`, patch);
  }

  public delete(id: number): Promise<AxiosResponse> {
    return Axios.delete(`${this.api}/groups/${id}`);
  }
}

// export a singleton instance in the global namespace
export const GroupServiceInstance = GroupService.Instance;
