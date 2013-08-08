<h3 class="glyphicons retweet"><i></i> Blog<span>| Latest from my blog</span></h3>
<div class="row-fluid blog">
	<ul>
		<?php for ($i=1; $i<=6; $i++): ?>
		<li class="span4">
			<span class="item">
				<img src="<?php if (DEV): ?>theme/images/lifestyle/<?php echo mt_rand(1,3); ?>.jpg<?php else: ?>http://dummyimage.com/500x500/232323/ffffff&amp;text=photo<?php endif; ?>" alt="blog post" />
				<a href="#blog-item-<?php echo $i; ?>" class="hover">
					<span class="glyphicons link"><i></i></span>
					<strong>Lorep ipsum dolor</strong>
					<span>26/02/2013</span>
				</a>
			</span>
		</li>
		<?php endfor; ?>
	</ul>
</div>
<?php $page = isset($_GET['p']) ? $_GET['p'] : 1; ?>
<div class="pagination pagination-large margin-bottom-none">
	<ul>
		<li<?php if ($page - 1 == 0): ?> class="disabled"<?php endif; ?>><a href="ajax.php?section=blog-listing&amp;p=<?php echo $page-1 > 0 ? $page - 1 : $page; ?>">&laquo;</a></li>
		<li<?php if ($page == 1): ?> class="active"<?php endif; ?>><a href="ajax.php?section=blog-listing&amp;p=1">1</a></li>
		<li<?php if ($page == 2): ?> class="active"<?php endif; ?>><a href="ajax.php?section=blog-listing&amp;p=2">2</a></li>
		<li<?php if ($page == 3): ?> class="active"<?php endif; ?>><a href="ajax.php?section=blog-listing&amp;p=3">3</a></li>
		<li<?php if ($page + 1 > 3): ?> class="disabled"<?php endif; ?>><a href="ajax.php?section=blog-listing&amp;p=<?php echo $page + 1; ?>">&raquo;</a></li>
	</ul>
</div>