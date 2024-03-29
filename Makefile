DOCS_DIR   := ./docs
SRC_DIR    := ./src
BUILD_DIR  := ./build
PUBLIC_DIR := ./public

PROJ := $(DOCS_DIR)/docs.fsproj

all: watch

.PHONY: build
build: css js
	@cp $(DOCS_DIR)/index.html $(PUBLIC_DIR)/index.html

.PHONY: watch
watch:
	@npx tailwindcss -i $(DOCS_DIR)/styles.css -o $(PUBLIC_DIR)/styles.css --watch=always &
	@dotnet fable watch $(PROJ) -o $(BUILD_DIR) --noRestore &
	@npx webpack serve

.PHONY: js
js:
	@dotnet fable $(PROJ) -o $(BUILD_DIR)

.PHONY: css
css:
	@npx tailwindcss -i $(DOCS_DIR)/styles.css -o $(PUBLIC_DIR)/styles.css

.PHONY: restore
restore:
	@npm install
	@dotnet tool restore
	@dotnet restore $(SRC_DIR)
	@dotnet restore $(DOCS_DIR)

.PHONY: clean
clean:
	@dotnet clean $(PROJ)
	@rm -rf $(PUBLIC_DIR)/*

.PHONY: remove
remove: clean
	@rm -rf $(DOCS_DIR)/obj/ $(DOCS_DIR)/bin/
	@rm -rf $(SRC_DIR)/obj/ $(SRC_DIR)/bin/
	@rm -rf $(BUILD_DIR)/*
	@rm -rf ./node_modules
	@rm -f  ./*-lock.*
