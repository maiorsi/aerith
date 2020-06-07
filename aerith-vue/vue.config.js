const path = require("path");

module.exports = {
  transpileDependencies: ["vuetify"],
  configureWebpack: {
    resolve: {
      extensions: [".js", ".vue"],
      alias: {
        fs: path.resolve(__dirname, "src/mock-fs.js")
      }
    }
  }
};
