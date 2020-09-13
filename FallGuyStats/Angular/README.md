# FallGuyStats
The Front end for the Fall Guy Stats Tool.
This uses the API to get stats to display and be viewable in the OBS browser source.
To learn more about the API and learn how you can contribute, go to the [Fall Guy Stats API github page](https://github.com/lealeelu/FallGuyStats-Api).

![Fall guy stats showing in obs](https://github.com/lealeelu/FallGuyStats-Api/blob/media/StatsExample.png)

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 10.0.8.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).

#File server for player.log
## Install http-server

Run `npm install -g http-server`
Navigate to the location of your player.log file: `cd C:\Users\lealeelu\AppData\LocalLow\Mediatonic\FallGuys_client`
Run the fileserver `http-server ./ --cors`
The cors flag is to allow cross-domain referencing from localhost:4200.
