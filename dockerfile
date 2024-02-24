#FROM mcr.microsoft.com/dotnet/sdk:8.0
FROM jupyter/base-notebook:x86_64-ubuntu-22.04

LABEL maintainer="George Chen <monstercoder@gmail.com>"
SHELL ["/bin/bash", "-o", "pipefail", "-c"]
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 \
    PATH="$PATH:/usr/share/tools"

USER root
RUN apt-get update --yes && \
    apt-get install --yes --no-install-recommends \
    curl \
    git \
    ca-certificates \
    libatomic1 \
    wget \
    && apt-get clean && rm -rf /var/lib/apt/lists/*

RUN mkdir -p /usr/share/dotnet \
    && curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0 -InstallDir /usr/share/dotnet \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet 

COPY notebooks /home/${NB_USER}/notebooks
COPY themes.jupyterlab-settings.json /home/${NB_USER}/.jupyter/lab/user-settings/@jupyterlab/apputils-extension/themes.jupyterlab-settings
COPY tracker.jupyterlab-settings.json /home/${NB_USER}/.jupyter/lab/user-settings/@jupyterlab/notebook-extension/tracker.jupyterlab-settings
RUN mkdir -p /usr/share/tools && mkdir /usr/share/jupyter/&& mkdir /usr/share/jupyter/kernels \
    && dotnet tool install Microsoft.dotnet-interactive --add-source / --tool-path /usr/share/tools \
    && dotnet interactive jupyter install --path /usr/share/jupyter/kernels \
    && chmod 777 -R /home/${NB_USER}

USER ${NB_UID}

# COPY overrides.json /home/jovyan/.jupyter/lab/user-settings/overrides.json
WORKDIR /home/${NB_USER}/notebooks


RUN conda install -y nbgitpuller



