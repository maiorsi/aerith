import axios, { AxiosResponse } from "axios";
import Credential from "@/models/credentials.interface";
import Token from "@/models/token.interface";

class AuthService {
  private static instance: AuthService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public login(credential: Credential): Promise<AxiosResponse<Token>> {
    return axios.post<Credential, AxiosResponse<Token>>(
      `https://localhost:5001/api/v1/auth/login`,
      credential
    );
  }
}

// export a singleton instance in the global namespace
export const AuthServiceInstance = AuthService.Instance;
