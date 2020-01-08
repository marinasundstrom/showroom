const path = require('path');
const webpack = require('webpack');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
var glob = require("glob");

module.exports = (env, args) => ({
    resolve: {
        extensions: ['.ts', '.js']
    },
    devtool: args.mode === 'development' ? 'inline-source-map' : 'none',
    plugins: [
        new CleanWebpackPlugin(),
        new MiniCssExtractPlugin({
            filename: '[name].css'
        })
    ],
    module: {
        rules: [
            {
                test: /\.ts?$/,
                loader: 'ts-loader'
            },
            {
                test: /\.s?css$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    {
                        loader: 'css-loader'
                    },
                    {
                        loader: 'resolve-url-loader'
                    },
                    {
                        loader: 'sass-loader',
                        options: {
                            sourceMap: true,
                            sourceMapContents: false
                        }
                    }
                ]
            },
            {
                test: /\.(jpe?g|png|gif|svg)$/i,
                loader: 'file-loader',
                options: {
                    name: 'images/[name].[ext]',
                },
            },
            {
                test: /\.(eot|otf|ttf|woff)$/i,
                loader: 'file-loader',
                options: {
                    name: 'fonts/[name].[ext]',
                },
            }
        ]
    },
    entry: {
        "main": glob.sync("./Scripts/*.ts"),
        "site": "./Styles/site.scss"
    },
    output: {
        path: path.join(__dirname, '/wwwroot/dist')
    }
});