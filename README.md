# Document Manager

## Description
Simple service that allows the creation of documents alongwith user and organization management.

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Tests](#tests)

## Installation

```bash
git clone https://github.com/CargoReligion/ppcodingassignment.git
cd ppcodingassignment
docker-compose up server
```

## Usage
You can either curl at http://localhost:8080 or run swagger (http://localhost:8080/swagger/index.html)

If you want to inspect the database locally, a nifty little tool called Adminer is also included in the docker-compose file
```bash
docker-compose up adminer
```
Once that is spun up, go to http://localhost:8081 and login using DB credentials specified in docker-compose environment variables

## Tests
A few basic unit tests are available and can be run via docker-compose

```bash
docker-compose up test
```
