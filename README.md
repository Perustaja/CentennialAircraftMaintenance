# CentennialAircraftMaintenance
MVC application with a few side services

# Requirements
This project requires user-secrets for development and will use azure key vault during production:
- SendGridKey & SendGridUser used in EmailSender
- FspUser & FspPass required for logging in to FSP, used by FspScraper

# Development environment
- All databases are currently SQLite and will use SQL server probably through azure
- Static files are currently just stored in the repo, but will be hooked up with blob storage once the parts and discrepancies section is finished
