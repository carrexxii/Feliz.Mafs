const path = require('path')

module.exports = {
  mode: "development",
  entry: {
    main: './build/main.js',
  },
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, 'public'),
  },
  devServer: {
    static: {
      directory: path.join(__dirname, 'public'),
    },
    port: 5000,
  },
}
