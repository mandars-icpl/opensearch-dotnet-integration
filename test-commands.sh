#!/bin/bash

# Base URL for the API
API_BASE="http://localhost:5171"

# Test getting all indices
echo "Testing GET /opensearch/indices"
curl -X GET "${API_BASE}/opensearch/indices"
echo -e "\n"

# Test getting all students
echo "Testing GET /students/"
curl -X GET "${API_BASE}/students/"
echo -e "\n"

# Direct OpenSearch API calls (requires curl with SSL support)
# Create test student document directly in OpenSearch
echo "Creating test student document directly in OpenSearch"
curl -k -X POST "https://localhost:9200/students/_doc" \
  -H "Content-Type: application/json" \
  -u "admin:C#abcd@1234" \
  -d '{
    "Id": 1002,
    "FirstName": "Jane",
    "LastName": "Smith",
    "GradYear": 2023,
    "Gpa": 3.92
  }'
echo -e "\n"
