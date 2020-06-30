import Axios, { AxiosResponse } from "axios";
import Code from "@/models/code.interface";
import Patch from '@/models/meta/patch.interface';
import { BaseService } from './base.service';

class CodeService extends BaseService {
  private static instance: CodeService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<Code[]>> {
    return Axios.get(`${this.api}/codes`);
  }

  public post(code: Code): Promise<AxiosResponse<Code>> {
    return Axios.post(`${this.api}/codes`, code);
  }

  public put(id: number, code: Code): Promise<AxiosResponse<Code>> {
    return Axios.put(`${this.api}/codes/${id}`, code);
  }

  public patch(id: number, patch: Patch[]): Promise<AxiosResponse<Code>> {
    return Axios.patch(`${this.api}/codes/${id}`, patch);
  }

  public delete(id: number): Promise<AxiosResponse> {
    return Axios.delete(`${this.api}/codes/${id}`);
  }
}

// export a singleton instance in the global namespace
export const CodeServiceInstance = CodeService.Instance;
