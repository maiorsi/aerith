import Axios, { AxiosResponse } from "axios";
import Code from "@/models/code.interface";

class CodeService {
  private static instance: CodeService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public get(): Promise<AxiosResponse<Code[]>> {
    return Axios.get("https://localhost:5001/api/v1/codes");
  }
}

// export a singleton instance in the global namespace
export const CodeServiceInstance = CodeService.Instance;
