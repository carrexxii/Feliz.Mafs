const path = require('path')

module.exports = {
  mode: "development",
  entry: {
    main: './build/src/main.js',
  },
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, 'wwwroot'),
  },
}