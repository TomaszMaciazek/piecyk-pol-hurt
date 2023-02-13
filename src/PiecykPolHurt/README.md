# Solution projects

This module consist of following project:
 * API - endpoints project
 * DataImport project - responsible for communication using gRPC
 * Model project - containig entities, dtos, queries, commands classes and enums that are used by the rest of solution projects
 * DataLayer project - persistance layer, containing DbContext and its migration classes and also unit of work pattern repositories
 * ApplicationLogic project - services layer project
 * Mappings project - contains automapper profile classes
 * Validation project - containing validator classes created with FluentValidation library

In order to run application correctly it is required to start API and DataImport projects.