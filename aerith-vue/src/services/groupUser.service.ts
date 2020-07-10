import Axios, { AxiosResponse } from "axios";
import GroupUser from "@/models/groupUser.interface";
import Patch from '@/models/meta/patch.interface';
import { BaseService } from './base.service';

class GroupUserService extends BaseService {
  private static instance: GroupUserService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<GroupUser[]>> {
    return Axios.get(`${this.api}/groupusers`);
  }

  public post(groupUser: GroupUser): Promise<AxiosResponse<GroupUser>> {
    return Axios.post(`${this.api}/groupusers`, groupUser);
  }

  public put(id: number, groupUser: GroupUser): Promise<AxiosResponse<GroupUser>> {
    return Axios.put(`${this.api}/groupusers/${id}`, groupUser);
  }

  public patch(id: number, patch: Patch[]): Promise<AxiosResponse<GroupUser>> {
    return Axios.patch(`${this.api}/groupusers/${id}`, patch);
  }

  public delete(id: number): Promise<AxiosResponse> {
    return Axios.delete(`${this.api}/groupusers/${id}`);
  }
}

// export a singleton instance in the global namespace
export const GroupUserServiceInstance = GroupUserService.Instance;
