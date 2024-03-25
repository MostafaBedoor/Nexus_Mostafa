const PROXY_CONFIG = [
  {
    context: [
      "/user",
    ],
    target: "https://localhost:7003",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
