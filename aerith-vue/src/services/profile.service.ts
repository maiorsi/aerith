class ProfileService {
  private static instance: ProfileService;

  public static get Instance() {
    // Do you need arguments? Make it a regular method instead.
    return this.instance || (this.instance = new this());
  }
}

// export a singleton instance in the global namespace
export const ProfileServiceInstance = ProfileService.Instance;
