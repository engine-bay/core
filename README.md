# EngineBay.Core

[![NuGet version](https://badge.fury.io/nu/EngineBay.Core.svg)](https://badge.fury.io/nu/EngineBay.Core)
[![Maintainability](https://api.codeclimate.com/v1/badges/df6af8a10863f4f0ba37/maintainability)](https://codeclimate.com/github/engine-bay/core/maintainability)
[![Test Coverage](https://api.codeclimate.com/v1/badges/df6af8a10863f4f0ba37/test_coverage)](https://codeclimate.com/github/engine-bay/core/test_coverage)

Core module for EngineBay published to [EngineBay.Core](https://www.nuget.org/packages/EngineBay.Core/) on NuGet.

## About

This module is used to store interfaces, exceptions, and other classes that are intended to be shared and used amongst many different EngineBay modules.

The interfaces allow for a module to request dependency injection for something without necessarily importing a module that implements that functionality. 