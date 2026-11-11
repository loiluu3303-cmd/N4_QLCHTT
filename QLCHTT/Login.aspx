<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.Login" %>
<!DOCTYPE html>
<html lang="vi">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Đăng nhập - FASHION</title>
    <link href="<%= ResolveUrl("~/Content/Site.css") %>" rel="stylesheet" />
    <style>
        :root { --fashion-green:#147b52; --fashion-dark:#103d30; --fashion-ink:#172b25; }
        * { box-sizing:border-box; }
        html, body { min-height:100%; }
        body.login-page {
            margin:0; min-height:100vh; color:var(--fashion-ink);
            font-family:Inter,"Segoe UI",Arial,sans-serif;
            background:#f3f6f4;
        }
        .login-shell {
            min-height:100vh; display:grid; grid-template-columns:minmax(380px, 1.05fr) minmax(440px, .95fr);
            padding:22px; gap:22px;
        }
        .login-showcase {
            position:relative; overflow:hidden; min-height:calc(100vh - 44px);
            border-radius:24px; padding:52px 56px; color:#fff;
            background:linear-gradient(145deg,#116b49 0%,#16885b 54%,#0d503c 100%);
            display:flex; flex-direction:column; justify-content:space-between;
            box-shadow:0 18px 50px rgba(16,75,52,.16);
        }
        .login-showcase:before,.login-showcase:after {
            content:""; position:absolute; border:1px solid rgba(255,255,255,.12);
            border-radius:50%; pointer-events:none;
        }
        .login-showcase:before { width:520px;height:520px;right:-180px;top:-160px; }
        .login-showcase:after { width:390px;height:390px;right:-105px;top:-95px; }
        .showcase-brand { position:relative; z-index:1; display:flex;align-items:center;gap:13px; }
        .showcase-logo { width:48px;height:48px;border-radius:14px;background:#fff;color:var(--fashion-green);
            display:grid;place-items:center;font-size:27px;font-weight:900;box-shadow:0 7px 20px rgba(0,0,0,.12); }
        .showcase-name { font-size:20px;font-weight:850;letter-spacing:1.2px;line-height:1.15; }
        .showcase-sub { font-size:9px;letter-spacing:2.2px;font-weight:700;opacity:.72;margin-top:5px; }
        .showcase-copy { position:relative;z-index:1;max-width:560px;margin:55px 0; }
        .showcase-kicker { display:inline-flex;align-items:center;gap:8px;padding:8px 12px;border:1px solid rgba(255,255,255,.24);
            border-radius:30px;background:rgba(255,255,255,.09);font-size:11px;letter-spacing:.7px; }
        .showcase-kicker i { width:7px;height:7px;border-radius:50%;background:#8de0b6;display:inline-block; }
        .showcase-copy h1 { font-size:clamp(32px,4vw,53px);line-height:1.12;letter-spacing:-1.4px;font-weight:800;margin:24px 0 17px; }
        .showcase-copy p { max-width:440px;color:rgba(255,255,255,.78);font-size:15px;line-height:1.8;margin:0; }
        .showcase-features { position:relative;z-index:1;display:flex;gap:22px;flex-wrap:wrap;margin-top:35px; }
        .feature-item { display:flex;align-items:center;gap:9px;font-size:12px;color:rgba(255,255,255,.88); }
        .feature-check { width:23px;height:23px;border-radius:50%;display:grid;place-items:center;background:rgba(255,255,255,.15);font-size:12px; }
        .showcase-footer { position:relative;z-index:1;color:rgba(255,255,255,.58);font-size:11px;letter-spacing:.2px; }
        .login-panel { display:flex;align-items:center;justify-content:center;padding:30px 5vw; }
        .login-card { width:100%;max-width:440px;background:#fff;border:1px solid #e8eeea;border-radius:20px;
            padding:42px 42px 34px;box-shadow:0 16px 45px rgba(25,54,41,.07); }
        .mobile-brand { display:none; }
        .login-eyebrow { color:var(--fashion-green);font-size:11px;font-weight:800;letter-spacing:1.5px;text-transform:uppercase;margin-bottom:11px; }
        .login-heading h2 { color:#1b2e26;font-size:29px;line-height:1.25;font-weight:800;letter-spacing:-.7px;margin:0 0 9px; }
        .login-heading p { color:#87938d;font-size:13px;line-height:1.6;margin:0 0 30px; }
        .login-field { margin-bottom:19px; }
        .login-field label { display:block;color:#405148;font-size:12px;font-weight:700;margin-bottom:9px; }
        .login-input { display:block;width:100%;height:49px;padding:0 15px;border:1px solid #dfe8e2;border-radius:9px;
            background:#fbfcfb;color:#24372e;font-size:13px;outline:none;transition:.2s; }
        .login-input:focus { border-color:#16865a;background:#fff;box-shadow:0 0 0 4px rgba(22,134,90,.10); }
        .login-input::placeholder { color:#aab4ae; }
        .login-message { display:block;color:#c0392b;font-size:12px;line-height:1.5;margin:0 0 12px; }
        .login-button { display:block;width:100%;height:49px;border:0;border-radius:9px;background:#168454;color:#fff;
            font-size:13px;font-weight:800;letter-spacing:.7px;cursor:pointer;transition:background .2s,transform .2s,box-shadow .2s; }
        .login-button:hover { background:#106b44;box-shadow:0 8px 18px rgba(22,132,84,.2);transform:translateY(-1px); }
        .login-note { display:flex;align-items:center;gap:8px;margin-top:22px;padding:12px 13px;border-radius:9px;background:#f3f8f5;
            color:#75847b;font-size:11px;line-height:1.5; }
        .login-note span { color:var(--fashion-green);font-size:15px; }
        .login-copyright { margin-top:28px;text-align:center;color:#a0aaa4;font-size:10px; }
        @media(max-width:900px) {
            .login-shell { grid-template-columns:1fr; padding:14px; }
            .login-showcase { min-height:280px;padding:30px;border-radius:18px; }
            .showcase-copy { margin:38px 0 25px; }
            .showcase-copy h1 { font-size:34px;margin:15px 0 10px; }
            .showcase-copy p { font-size:13px; }
            .showcase-features,.showcase-footer { display:none; }
            .login-panel { padding:15px 0 25px; }
        }
        @media(max-width:520px) {
            .login-shell { padding:0;gap:0; }
            .login-showcase { min-height:205px;border-radius:0 0 20px 20px;padding:23px 22px; }
            .showcase-logo { width:40px;height:40px;font-size:23px; }
            .showcase-name { font-size:17px; }
            .showcase-copy { margin:25px 0 5px; }
            .showcase-copy h1 { font-size:27px; }
            .showcase-copy p { font-size:12px; }
            .login-panel { padding:22px 15px 30px; }
            .login-card { padding:29px 23px 25px;border-radius:16px; }
            .login-heading h2 { font-size:25px; }
        }
    </style>
</head>
<body class="login-page">
<form id="formLogin" runat="server">
    <main class="login-shell">
        <section class="login-showcase">
            <div class="showcase-brand">
                <div class="showcase-logo">F</div>
                <div>
                    <div class="showcase-name">FASHION</div>
                    <div class="showcase-sub">ADMIN PANEL</div>
                </div>
            </div>

            <div class="showcase-copy">
                <div class="showcase-kicker"><i></i> HỆ THỐNG QUẢN LÝ CỬA HÀNG</div>
                <h1>Quản lý thông minh.<br />Vận hành hiệu quả.</h1>
                <p>Không gian quản trị tập trung giúp bạn theo dõi bán hàng, sản phẩm, kho và khách hàng một cách thuận tiện.</p>
                <div class="showcase-features">
                    <div class="feature-item"><span class="feature-check">✓</span> Quản lý bán hàng</div>
                    <div class="feature-item"><span class="feature-check">✓</span> Theo dõi tồn kho</div>
                    <div class="feature-item"><span class="feature-check">✓</span> Báo cáo tổng quan</div>
                </div>
            </div>

            <div class="showcase-footer">FASHION MANAGEMENT SYSTEM · © 2026</div>
        </section>

        <section class="login-panel">
            <div class="login-card">
                <div class="mobile-brand">
                    <div class="showcase-logo">F</div>
                </div>
                <div class="login-heading">
                    <div class="login-eyebrow">Chào mừng trở lại</div>
                    <h2>Đăng nhập</h2>
                    <p>Vui lòng nhập thông tin tài khoản để tiếp tục vào hệ thống.</p>
                </div>

                <div class="login-field">
                    <label for="txtTenDangNhap">Tên đăng nhập</label>
                    <asp:TextBox ID="txtTenDangNhap" runat="server" CssClass="login-input"
                        placeholder="Nhập tên đăng nhập" MaxLength="50" />
                </div>
                <div class="login-field">
                    <label for="txtMatKhau">Mật khẩu</label>
                    <asp:TextBox ID="txtMatKhau" runat="server" TextMode="Password"
                        CssClass="login-input" placeholder="Nhập mật khẩu" MaxLength="255" />
                </div>
                <asp:Label ID="lblThongBao" runat="server" CssClass="login-message" EnableViewState="false" />
                <asp:Button ID="btnDangNhap" runat="server" Text="ĐĂNG NHẬP VÀO HỆ THỐNG"
                    CssClass="login-button" OnClick="btnDangNhap_Click" />

                <div class="login-note"><span>ⓘ</span> Tài khoản được xác thực từ cơ sở dữ liệu của cửa hàng.</div>
                <div class="login-copyright">© 2026 FASHION · Hệ thống quản lý cửa hàng thời trang</div>
            </div>
        </section>
    </main>
</form>
</body>
</html>
