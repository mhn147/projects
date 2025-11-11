<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link rel="icon" type="image/png" href="./favicon.png" />
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous" />
  <link rel="stylesheet" href="css/main.css" />
  <title>Nasri Mohamed</title>
</head>

<body>
  <header class="less-dark-background-color">
    <nav class="container navbar navbar-expand-lg navbar-dark p-1">
      <div class="container-fluid px-0">
        <a class="navbar-brand brand" href="index.php">
          <img src="img/logo.svg" class="brand-img" alt="Website Logo: Uppercase letter N" />
        </a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0 navbar-items-container">
            <li class="nav-item text-center mx-2 only-on-collapse">
              <a class="nav-link p-2" href="#projects"> Projects </a>
            </li>
            <li class="nav-item text-center only-on-collapse mb-3">
              <a class="nav-link p-2" href="#skills"> Skills </a>
            </li>
            <li class="nav-item text-center">
              <a class="btn btn-outline-light p-2 cta-btn" href="contact.php">
                Contact
              </a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </header>

  <main class="container my-5">
    <div class="row">
      <div class="col-12 col-sm-8">
        <div class="row">
          <div class="col-12 hero-container">
            <section class="hero m-2 round-border text-white p-5 less-dark-background-color">
              <h1 class="text-center mb-4">
                Hello. <span class="text-primary"></span>I'm Mohamed ;&#41;
              </h1>
              <div class="row">
                <div class="col-12 col-sm-8 about-me-container">
                  <section id="about-me">
                    <p>
                      Hi there! My name is Mohamed Nasri and I’m a FullStack
                      Web Developer. I’m based in Tunis, Tunisia and I’m
                      looking for a dev role.
                    </p>
                    <p>
                      My Fascination with computers and software has led me to
                      become a developer.
                    </p>
                    <p>
                      I enjoy Coding and find it a fulfilling activity and I'm
                      always looking forward to learn or build something new.
                    </p>
                  </section>
                </div>
                <div class="col-12 col-sm-4 personal-pic-container">
                  <section class="personal-pic d-flex h-100 justify-content-center align-items-center">
                    <img class="personal-pic-img" src="img/personalPic.jpg" alt="personal picture" />
                  </section>
                </div>
              </div>
            </section>
          </div>
          <div class="col-12 skills-container p-0">
            <section id="skills" class="my-2 round-border py-4 px-4 text-white">
              <h2 class="text-center text-white mb-4">Skills</h2>
              <div class="row">
                <div class="col-12 col-sm-4 mb-4">
                  <div class="p-3 round-border less-dark-background-color">
                    <article class="skills">
                      <h4 class="skills-header text-center mb-2">
                        Front End
                      </h4>
                      <div class="w-100 skill-container front-end-bg">
                        <div class="row h-100 p-3 align-items-center">
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/html-logo.svg" alt="HTML" title="HTML" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/css-logo.svg" alt="CSS" title="CSS" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/js-logo.svg" alt="JavaScript" title="JavaScript" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/ts-logo.svg" alt="TypeScript" title="TypeScript" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/ng-logo.svg" alt="Angular" title="Angular" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/ionic-logo.svg" alt="Ionic Framework" title="Ionic Framework" />
                          </div>
                        </div>
                      </div>
                    </article>
                  </div>
                </div>
                <div class="col-12 col-sm-4 mb-4">
                  <div class="p-3 round-border less-dark-background-color">
                    <article class="skills">
                      <h4 class="skills-header text-center mb-2">Back End</h4>
                      <div class="w-100 skill-container back-end-bg">
                        <div class="row h-100 p-3 align-items-center">
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/c-sharp-logo.svg" alt="CSharp" title="CSharp" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/asp-core-logo.svg" alt="ASP.NET Core" title="ASP.NET Core" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/ef-logo.svg" alt="EntityFramework.NET Core" title="EntityFramework.NET Core" />
                          </div>
                          <div class="col-4 offset-2 d-flex justify-content-center">
                            <img class="icon" src="img/sql-logo.svg" alt="SQL" title="SQL" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/linux-logo.svg" alt="Linux Administration" title="Linux Administration" />
                          </div>
                        </div>
                      </div>
                    </article>
                  </div>
                </div>

                <div class="col-12 col-sm-4 mb-4">
                  <div class="p-3 round-border less-dark-background-color">
                    <article class="skills">
                      <h4 class="skills-header text-center mb-2">
                        Dev Tools
                      </h4>
                      <div class="w-100 skill-container tools-bg">
                        <div class="row h-100 p-3 align-items-center">
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/chrome-dt-logo.svg" title="Google Chrome Dev Tools" alt="Google Chrome Dev Tools" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/vs-code-logo.svg" title="Visual Studio Code" alt="Visual Studio Code" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/vs-logo.svg" alt="Visual Studio" title="Visual Studio" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/git-logo.svg" alt="Git" title="Git" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/github-logo.svg" alt="Github" title="Github" />
                          </div>
                          <div class="col-4 d-flex justify-content-center">
                            <img class="icon" src="img/xd-logo.svg" alt="Adobe Xd" title="Adobe Xd" />
                          </div>
                        </div>
                      </div>
                    </article>
                  </div>
                </div>
              </div>
            </section>
          </div>
        </div>
      </div>
      <div class="col-12 col-sm-4 projects-container">
        <section id="projects" class="projects m-2 round-border text-white py-5 less-dark-background-color">
          <h2 class="text-center mb-4">Projects</h2>

          <div class="row">
            <div class="col-12 p-0">
              <article class="project card darker-background-color">
                <div class="card-body">
                  <h4 class="text-white text-center">
                    Mobile App - Permaflex
                  </h4>
                  <p class="card-text text-white">
                    I Was in charge of developing the UI of the
                    <a href="http://permaflex.com.tn">Permaflex Tunisie</a>
                    Mobile App using <strong>Ionic(Angular)</strong>.
                  </p>
                  <div class="d-flex justify-content-center align-items-center">
                    <div class="btn-group">
                      <a type="button" class="btn btn-outline-light" href="https://play.google.com/store/apps/details?id=permaflex.bacha.io">App on Google Play</a>
                    </div>
                  </div>
                </div>
              </article>
              <article class="project mt-3 card darker-background-color">
                <div class="card-body">
                  <h4 class="text-white text-center">
                    Document Management System
                  </h4>
                  <p class="card-text text-white">
                    During my first intership, I developed a small Document
                    Management System. Server Side with
                    <strong>.NET Core 2.1</strong> and the UI is built with
                    <strong>Vue.js</strong>
                  </p>
                  <div class="d-flex justify-content-center align-items-center">
                    <div class="btn-group">
                      <a type="button" class="btn btn-outline-light" href="https://gitlab.com/mhn147/ged/-/tree/master">Code</a>
                    </div>
                  </div>
                </div>
              </article>
              <article class="project mt-3 card darker-background-color">
                <div class="card-body">
                  <h4 class="text-white text-center">TinyPDF</h4>
                  <p class="card-text text-white">
                    This is my most ambitious project. It's a Python Flask app
                    that uses GhostScript to compress PDF files.
                  </p>
                  <div class="d-flex justify-content-center align-items-center">
                    <div class="btn-group">
                      <a type="button" class="btn btn-outline-light" href="https://tinypdf.hamouda.tn">Live</a>
                      <a type="button" class="btn btn-outline-light" href="https://github.com/mhn147/tinyPDF">Code</a>
                    </div>
                  </div>
                </div>
              </article>
              <article class="project mt-3 card darker-background-color">
                <div class="card-body">
                  <h4 class="text-white text-center">EasyPast</h4>
                  <p class="card-text text-white">
                    It sounds amazing and complicated, but it's actually not.
                    I used a free web API that shrinks URLs, check it out!
                  </p>
                  <div class="d-flex justify-content-center align-items-center">
                    <div class="btn-group">
                      <a type="button" class="btn btn-outline-light" href="https://easy-past-v3.netlify.app/">Live</a>
                      <a type="button" class="btn btn-outline-light" href="https://github.com/mhn147/easy-past">Code</a>
                    </div>
                  </div>
                </div>
              </article>
            </div>
          </div>
        </section>
      </div>
    </div>
  </main>

  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/js/bootstrap.bundle.min.js" integrity="sha384-b5kHyXgcpbZJO/tY9Ul7kGkf1S0CWuKcCD38l8YkeH8z8QjE0GmW1gYU5S9FOnJ0" crossorigin="anonymous"></script>
</body>

</html>