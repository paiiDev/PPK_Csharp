function getData() {
  let data = localStorage.getItem("blogs");
  if (data) {
    return JSON.parse(data);
  } else {
    return [];
  }
}

function saveData(list) {
  localStorage.setItem("blogs", JSON.stringify(list));
}

function createBlog(title, author, content) {
  const list = getData();

  const blog = {
    id: crypto.randomUUID(),
    title: title,
    author: author,
    content: content,
  };

  list.push(blog);
  saveData(list);
  return blog;
}

function updateBlog(id, title, author, content) {
  const list = getData();

  const index = list.findIndex((blog) => blog.id === id);
  if (index === -1) return;

  list[index] = { id, title, author, content };
  saveData(list);
}

function deleteBlog(id) {
  let list = getData();
  list = list.filter((blog) => blog.id !== id);
  saveData(list);
}

function getBlogs() {
  return getData();
}

//event handling
$(window).on("load", function () {
  renderBlogs();
});

let editId = null;

$("#submitBtn").click(function () {
  console.log("button clicked");
  const title = $("#BlogTitle").val();
  const author = $("#Author").val();
  const content = $("#BlogContent").val();

  if (
    title.trim().length === 0 ||
    author.trim().length === 0 ||
    content.trim().length === 0
  ) {
    alert("Please enter require fields");
  } else {
    if (editId === null) {
      createBlog(title, author, content);
      renderBlogs();
      clearFields();
    } else {
      updateBlog(editId, title, author, content);
      editId = null;
      renderBlogs();
      clearFields();
    }
  }
});

$("#cancelBtn").click(function () {
  clearFields();
});

function clearFields() {
  $("#BlogTitle").val("");
  $("#Author").val("");
  $("#BlogContent").val("");
}

//edit and delete button

$(document).on("click", ".btn-edit", function () {
  console.log("edit button clicked.");
  const id = $(this).data("id");
  console.log(id);
  const blog = getBlogs().find((x) => x.id === id);
  console.log(blog);

  $("#BlogTitle").val(blog.title);
  $("#Author").val(blog.author);
  $("#BlogContent").val(blog.content);

  editId = id;
});

$(document).on("click", ".btn-delete", function () {
  const id = $(this).data("id");
  console.log("delete id" + id);

  if (confirm("Delete this blog?")) {
    deleteBlog(id);
    console.log("Deleted from storage. Now rendering...");
    renderBlogs();
    console.log("Render function finished.");
  }

  return;
});
