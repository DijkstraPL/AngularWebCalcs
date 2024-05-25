# 3. SYS - Use modular monolith

Date: 2019-10-08

## Status

Accepted

## Context

- Hard to maintain one big module of an application.

## Decision

Keep small modules as a containers for functionalities.

## Consequences

- Positive
	- It is better to have it as several smaller modules in terms of readability and future work.
- Negative
	- .Net Core 3.0 not allows to create Class libraries for WPF stuff.
