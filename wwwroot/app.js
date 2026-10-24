const filterForm = document.querySelector("#course-filters");
const results = document.querySelector("#course-results");
const resultsCount = document.querySelector("#results-count");
const message = document.querySelector("#course-message");
const detailSection = document.querySelector("#course-detail");
const navigation = document.querySelector("#main-navigation");
const menuToggle = document.querySelector(".menu-toggle");
const header = document.querySelector(".site-header");
const brandLogo = document.querySelector(".site-header .brand img");

let searchTimeout;

function escapeHtml(value) {
  return String(value).replace(/[&<>"']/g, (character) => ({
    "&": "&amp;",
    "<": "&lt;",
    ">": "&gt;",
    '"': "&quot;",
    "'": "&#39;"
  })[character]);
}

function setLoading(isLoading) {
  results.setAttribute("aria-busy", String(isLoading));
}

function getFilters() {
  return {
    search: document.querySelector("#search").value.trim(),
    category: document.querySelector("#category").value,
    modality: document.querySelector("#modality").value,
    status: document.querySelector("#status").value
  };
}

function courseCard(course) {
  return `
    <article class="course-card">
      <a class="course-card-link" href="#curso/${encodeURIComponent(course.id)}" aria-label="Ver detalhes: ${escapeHtml(course.name)}">
        <div class="course-image-wrap">
          <img class="course-image" src="${escapeHtml(course.image)}" alt="" loading="lazy">
          <span class="category-label">${escapeHtml(course.category)}</span>
        </div>
        <div class="course-card-content">
          <div class="course-meta">
            <span>${escapeHtml(course.modality)}</span>
            <span class="status-badge" data-status="${escapeHtml(course.status)}">${escapeHtml(course.status)}</span>
          </div>
          <h3>${escapeHtml(course.name)}</h3>
          <p class="course-card-description">${escapeHtml(course.description)}</p>
          <div class="course-card-footer">
            <span>${course.workload} horas · Certificação ${escapeHtml(course.certification)}</span>
            <span class="card-action">Saiba mais →</span>
          </div>
        </div>
      </a>
    </article>`;
}

async function loadCourses() {
  if (location.hash.startsWith("#curso/")) return;

  setLoading(true);
  message.hidden = true;
  results.hidden = false;
  resultsCount.textContent = "Carregando cursos…";

  try {
    const params = new URLSearchParams();
    for (const [key, value] of Object.entries(getFilters())) {
      if (value) params.set(key, value);
    }

    const response = await fetch(`/api/courses?${params.toString()}`);
    if (!response.ok) throw new Error(`A API respondeu com status ${response.status}.`);
    const courses = await response.json();

    results.replaceChildren();
    if (courses.length === 0) {
      results.hidden = true;
      message.innerHTML = "<strong>Nenhum curso encontrado</strong>Tente alterar os termos da pesquisa ou limpar os filtros.";
      message.hidden = false;
      resultsCount.textContent = "0 cursos encontrados";
      return;
    }

    results.innerHTML = courses.map(courseCard).join("");
    resultsCount.textContent = `${courses.length} ${courses.length === 1 ? "curso encontrado" : "cursos encontrados"}`;
  } catch (error) {
    results.hidden = true;
    message.innerHTML = "<strong>Não foi possível carregar os cursos</strong>Verifique sua conexão e tente novamente.";
    message.hidden = false;
    resultsCount.textContent = "Ocorreu um erro ao consultar o catálogo.";
    console.error("Falha ao carregar o catálogo de cursos:", error);
  } finally {
    setLoading(false);
  }
}

function formatDate(date) {
  if (!date) return "A definir";
  const [year, month, day] = date.split("-").map(Number);
  return new Intl.DateTimeFormat("pt-BR").format(new Date(year, month - 1, day));
}

function detailMarkup(course) {
  const modules = course.modules?.length
    ? course.modules.map((module, index) => `
      <article class="module-card">
        <h3>Módulo ${index + 1}: ${escapeHtml(module.name)}</h3>
        <p>${escapeHtml(module.description)}</p>
        <ul>${module.lessons.map((lesson) => `<li>${escapeHtml(lesson)}</li>`).join("")}</ul>
      </article>`).join("")
    : "<p>Conteúdo programático em breve.</p>";

  return `
    <a class="detail-back" href="#cursos"><span aria-hidden="true">←</span> Voltar para cursos</a>
    <div class="detail-hero">
      <div>
        <span class="detail-category">${escapeHtml(course.category)}</span>
        <h1 class="detail-title">${escapeHtml(course.name)}</h1>
        <div class="detail-facts">
          <p><strong>Início do curso:</strong> ${formatDate(course.availableDate)}</p>
          <p><strong>Certificação:</strong> ${escapeHtml(course.certification)}</p>
          <p><strong>Modalidade:</strong> ${escapeHtml(course.modality)} · ${course.workload} horas</p>
          <p><strong>Status:</strong> ${escapeHtml(course.status)}</p>
        </div>
        <a class="button button-dark" href="#cursos">Conheça outros cursos <span aria-hidden="true">→</span></a>
      </div>
      <div class="detail-media">
        <img src="${escapeHtml(course.image)}" alt="">
        <span class="detail-play" aria-hidden="true">▶</span>
      </div>
    </div>
    <div class="detail-benefits" aria-label="Benefícios dos cursos SESI">
      <article class="detail-benefit">
        <img src="/assets/01-certificacao-bege.svg" alt="" width="34" height="34">
        <h3>Certificação SESI</h3><p>Certificação reconhecida pelo mercado industrial.</p>
      </article>
      <article class="detail-benefit">
        <img src="/assets/02-professores-bege.svg" alt="" width="34" height="34">
        <h3>Professores especialistas</h3><p>Aprenda com profissionais experientes.</p>
      </article>
      <article class="detail-benefit">
        <img src="/assets/03-suporte-bege.svg" alt="" width="34" height="34">
        <h3>Suporte contínuo</h3><p>Acompanhamento durante todo o curso.</p>
      </article>
      <article class="detail-benefit">
        <img src="/assets/04-flexibilidade-bege.svg" alt="" width="34" height="34">
        <h3>Flexibilidade</h3><p>Formação para diferentes rotinas de trabalho.</p>
      </article>
    </div>
    <div class="detail-description">
      <p class="eyebrow">Sobre a formação</p>
      <h2>O que você vai aprender</h2>
      <p>${escapeHtml(course.description)}</p>
      <p class="course-audience"><strong>Para quem é:</strong> ${escapeHtml(course.targetAudience)}</p>
      <h2 class="curriculum-heading">Conteúdo programático</h2>
      <div class="curriculum">${modules}</div>
    </div>`;
}

async function loadCourseDetail(id) {
  results.hidden = true;
  document.querySelector(".courses-section").hidden = true;
  document.querySelector(".benefits-section").hidden = true;
  document.querySelector(".student-section").hidden = true;
  detailSection.hidden = false;
  detailSection.innerHTML = '<p role="status">Carregando informações do curso…</p>';
  detailSection.focus({ preventScroll: true });
  window.scrollTo({ top: 0, behavior: "smooth" });

  try {
    const response = await fetch(`/api/courses/${encodeURIComponent(id)}`);
    if (!response.ok) {
      if (response.status === 404) throw new Error("Esse curso não foi encontrado no catálogo.");
      throw new Error("Não foi possível consultar os detalhes agora.");
    }
    const course = await response.json();
    detailSection.innerHTML = detailMarkup(course);
  } catch (error) {
    detailSection.innerHTML = `
      <a class="detail-back" href="#cursos"><span aria-hidden="true">←</span> Voltar para cursos</a>
      <p class="detail-error" role="alert">${escapeHtml(error.message)}</p>`;
    console.error("Falha ao carregar os detalhes do curso:", error);
  }
}

function renderRoute() {
  const match = location.hash.match(/^#curso\/([^/?#]+)/);
  const isDetail = Boolean(match);
  document.querySelector(".hero").hidden = isDetail;
  header.classList.toggle("is-detail", isDetail);
  brandLogo.src = isDetail ? "/assets/logo-sesi-industria-primary.svg" : "/assets/logo-sesi-industria-white.svg";
  detailSection.hidden = !isDetail;
  document.querySelector(".courses-section").hidden = isDetail;
  document.querySelector(".benefits-section").hidden = isDetail;
  document.querySelector(".student-section").hidden = isDetail;

  if (isDetail) {
    loadCourseDetail(decodeURIComponent(match[1]));
  } else {
    results.hidden = false;
    loadCourses();
    if (location.hash === "#cursos") {
      window.requestAnimationFrame(() => {
        document.querySelector("#cursos").scrollIntoView({ behavior: "smooth" });
      });
    }
  }
}

filterForm.addEventListener("submit", (event) => {
  event.preventDefault();
  loadCourses();
});

filterForm.addEventListener("change", loadCourses);
document.querySelector("#search").addEventListener("input", () => {
  window.clearTimeout(searchTimeout);
  searchTimeout = window.setTimeout(loadCourses, 250);
});

filterForm.addEventListener("reset", () => {
  window.setTimeout(loadCourses, 0);
});

menuToggle.addEventListener("click", () => {
  const isOpen = navigation.classList.toggle("is-open");
  menuToggle.setAttribute("aria-expanded", String(isOpen));
  menuToggle.querySelector(".sr-only").textContent = isOpen ? "Fechar menu" : "Abrir menu";
});

navigation.addEventListener("click", (event) => {
  if (event.target instanceof HTMLAnchorElement) {
    navigation.classList.remove("is-open");
    menuToggle.setAttribute("aria-expanded", "false");
    menuToggle.querySelector(".sr-only").textContent = "Abrir menu";
  }
});

window.addEventListener("hashchange", renderRoute);
renderRoute();
