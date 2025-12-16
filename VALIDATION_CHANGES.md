# strict Input Validation Updates

This document details the recent updates made to the KerjaNusantara application to improve data integrity and user experience through strict input validation.

## 1. Core Utilities (`ConsoleHelper.cs`)
We upgraded the `ConsoleHelper` class to support robust input handling:

*   **Looping Logic**: Methods now loop indefinitely until valid data is entered. This prevents the application from crashing or accepting invalid default values (like `0`) when users make typos.
*   **New `ReadInput` Overload**: Added a flexible method `ReadInput(prompt, validator, errorMessage)` that allows any part of the application to define custom rules (e.g., "letters only", "min length").
*   **Numeric Safety**: `ReadInt` and `ReadDecimal` now strictly reject non-numeric input and prompt the user to try again.

## 2. Citizen Portal Changes
*   **Registration**:
    *   **NIK**: Now strictly requires **exactly 16 digits**. Inputs with letters or wrong lengths are rejected.
    *   **Name**: Now strictly requires **letters and spaces only**. Numbers or symbols are rejected.
    *   **Email**: Must contain an `@` symbol.

## 3. Company Portal Changes
*   **Registration**:
    *   **Contact Person/Industry**: Letters only.
    *   **Email**: Format validation.
    *   **Company Name/Reg Number**: Cannot be empty.
*   **Job Posting**:
    *   **Job Title/Location**: Non-empty validation.
    *   **Description**: Minimum length of **10 characters** to ensure quality postings.
*   **Bidding**:
    *   **Proposal**: Minimum length of **20 characters**.

## 4. Government Portal Changes
*   **Registration**:
    *   **Contact/Agency/Department**: Validated for letters/non-empty as appropriate.
*   **Project Creation**:
    *   **Description**: Minimum length of **20 characters**.
    *   **Dates**: Strict format checking for dates (yyyy-MM-dd), with a retry loop on invalid formats.

## Summary of Impact
Failed inputs no longer result in silent errors or bad data. The user is always explicitly told what went wrong (e.g., "NIK must be 16 digits") and is immediately given a chance to correct it.
