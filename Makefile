PROJ := ./sharpmafs.fsproj

SRC_DIR   := ./src
BUILD_DIR := ./build
WWW_ROOT  := ./wwwroot

all: watch

.PHONY: build
build: css js
	@cp ./index.html $(WWW_ROOT)/index.html

.PHONY: watch
watch:
	@trap '$(foreach count, $(shell seq 1 3), kill %$(count);)' SIGINT
	@npx tailwindcss -i ./styles.css -o $(WWW_ROOT)/styles.css --watch=always &
	@dotnet fable watch $(PROJ) -o $(BUILD_DIR) --noRestore --silent &
	@npx webpack serve

.PHONY: js
js:
	@dotnet fable $(PROJ) -o $(BUILD_DIR)/.

.PHONY: css
css:
	@npx tailwindcss -i ./styles.css -o $(WWW_ROOT)/styles.css

.PHONY: restore
restore:
	@npm install
	@dotnet tool restore
	@dotnet restore

.PHONY: clean
clean:
	@dotnet clean
	@dotnet fable clean --yes
	@rm -rf $(WWW_ROOT)/*

.PHONY: remove
remove: clean
	@rm -rf ./obj/ ./bin/
	@rm -rf ./build/*
	@rm -rf ./node_modules
	@rm -f ./*-lock.*
