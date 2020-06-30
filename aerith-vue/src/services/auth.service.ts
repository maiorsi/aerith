import Axios, { AxiosResponse } from "axios";
import Credential from "@/models/credentials.interface";
import Token from "@/models/token.interface";
import { BaseService } from './base.service';

class AuthService extends BaseService {
  private static instance: AuthService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }

  public login(credential: Credential): Promise<AxiosResponse<Token>> {
    return Axios.post<Credential, AxiosResponse<Token>>(
      `${this.api}/auth/login`,
      credential
    );
  }

  public refresh(token: Token): Promise<AxiosResponse<Token>> {
    return Axios.post<Token, AxiosResponse<Token>>(
      `${this.api}/auth/refresh`,
      token
    );
  }
}

// export a singleton instance in the global namespace
export const AuthServiceInstance = AuthService.Instance;
